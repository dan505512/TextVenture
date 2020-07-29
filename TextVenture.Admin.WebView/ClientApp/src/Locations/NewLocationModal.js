/* eslint-disable eqeqeq */
import React, { useState, useEffect } from 'react';
import { MenuItem, Select, Modal, TextField, Paper, makeStyles, InputLabel, Button } from "@material-ui/core";
import _ from 'lodash';

const useStyles = makeStyles((theme) => ({
    modal: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    },
    paper: {
      backgroundColor: theme.palette.background.paper,
      border: '2px solid #000',
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3),
      height: '70%'
    },
    textField: {
        display: 'block'
    },
    submitButton: {
        marginTop: '10%'
    },
    select: {
        display: 'block',
    },
    selectLabel: {
        marginTop: '10%'
    }
  }));
// A modal to add or edit locations
export const NewLocationModal = ({isOpen, enemies, items, locations, chosenLocation, setClosed}) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [north, setNorth] = useState('');
    const [south, setSouth] = useState('');
    const [east, setEast] = useState('');
    const [west, setWest] = useState('')
    const [item, setItem] = useState('');
    const [enemy, setEnemy] = useState('');
    const [nameError, setNameError] = useState(false);
    const [descriptionError, setDescriptionError] = useState(false);
    const classes = useStyles();

    // When the chosen location changes, update all properties. This happens when edit is clicked or the modal is closed.
    useEffect(() => {
        setName(chosenLocation ? chosenLocation.name : '');
        setDescription(chosenLocation ? chosenLocation.description : '');
        setNorth(chosenLocation && chosenLocation.north ? chosenLocation.north : '')
        setSouth(chosenLocation && chosenLocation.south ? chosenLocation.south : '')
        setEast(chosenLocation && chosenLocation.east ? chosenLocation.east : '')
        setWest(chosenLocation && chosenLocation.west ? chosenLocation.west : '')
        setItem(chosenLocation && chosenLocation.item ? chosenLocation.item : '')
        setEnemy(chosenLocation && chosenLocation.enemy ? chosenLocation.enemy : '')
    }, [chosenLocation]);

    // Map any kind of object to a menu item
    const getMenuItems = list => (list.map(type => <MenuItem key={type.id} value={type.id}>{type.name}</MenuItem>))

    // Making sure input is valid before submitting
    const validateValues = () => {
        let isValid = true;

        if (_.isEmpty(name)) {
            isValid = false;
            setNameError(true);
        } else {
            setNameError(false);
        }

        if(_.isEmpty(description)) {
            isValid = false;
            setDescriptionError(true);
        } else {
            setDescriptionError(false);
        }

        return isValid;
    }

    // When creating a new location we send a POST request to the locations api, with all the data in the body
    const createNewLocation = async body => {
        const request = new Request('api/locations', { body, method: 'POST', headers: {"content-type": 'application/json'} });

        const res = await fetch(request)
        if(res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    // When editing a location we send all the data in the body but the id, which is sent as a query.
    const editLocation = async body => {
        const request = new Request(`api/locations?id=${chosenLocation.id}`, { body, method: 'PUT', headers: {"content-type": 'application/json'} });

        const res = await fetch(request)
        if(res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    // Validate data and decide if editing or adding.
    const onSubmit = () => {
        const canSubmit = validateValues();

        if (!canSubmit) {
            return;
        }
        
        const body = JSON.stringify({
            name,
            description,
            // For our api 0 is the same as null and we use == and not === cause this might be a string
            north: isNaN(north) || north == 0 ? null : Number(north),
            south: isNaN(south) || south == 0 ? null : Number(south),
            east: isNaN(east) || east == 0 ? null : Number(east),
            west: isNaN(west) || west == 0 ? null : Number(west),
            item: isNaN(item) || item == 0 ? null : Number(item),
            enemy: isNaN(enemy) || enemy == 0 ? null : Number(enemy),
        });

        chosenLocation ? editLocation(body) : createNewLocation(body);
    }

    return (
        <Modal open={isOpen} onClose={setClosed} className={classes.modal}>
            <Paper className={classes.paper}>
                <TextField value={name} onChange={e => setName(e.target.value)} label='name' className={classes.textField} error={nameError} />
                <TextField value={description} onChange={e => setDescription(e.target.value)} label='description' className={classes.textField} error={descriptionError}/>
                <InputLabel id='north-select-label' className={classes.selectLabel}>North</InputLabel>
                <Select labelId='north-select-label' value={north} onChange={e => setNorth(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(locations)}
                </Select>
                <InputLabel id='south-select-label' className={classes.selectLabel}>South</InputLabel>
                <Select labelId='south-select-label' value={south} onChange={e => setSouth(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(locations)}
                </Select>
                <InputLabel id='east-select-label' className={classes.selectLabel}>East</InputLabel>
                <Select labelId='east-select-label' value={east} onChange={e => setEast(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(locations)}
                </Select>
                <InputLabel id='west-select-label' className={classes.selectLabel}>West</InputLabel>
                <Select labelId='west-select-label' value={west} onChange={e => setWest(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(locations)}
                </Select>
                <InputLabel id='item-select-label' className={classes.selectLabel}>Item</InputLabel>
                <Select labelId='item-select-label' value={item} onChange={e => setItem(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(items)}
                </Select>
                <InputLabel id='enemy-select-label' className={classes.selectLabel}>Enemy</InputLabel>
                <Select labelId='enemy-select-label' value={enemy} onChange={e => setEnemy(e.target.value)} className={classes.select}>
                    <MenuItem value=''>None</MenuItem>
                    {getMenuItems(enemies)}
                </Select>
                <Button className={classes.submitButton} color='primary' variant='contained' onClick={onSubmit}>{chosenLocation ? "Edit" : "Add"}</Button>
            </Paper>
        </Modal>
    )
}