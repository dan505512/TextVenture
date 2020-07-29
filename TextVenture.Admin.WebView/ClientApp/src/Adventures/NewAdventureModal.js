import React, { useState, useEffect } from 'react';
import { Modal, TextField, Paper, Button, Select, MenuItem } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
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
        height: '35%'
    },
    textField: {
        display: 'block'
    },
    submitButton: {
        marginTop: '10%'
    },
    select: {
        display: 'block',
        marginTop: '10%'
    }
}));
// A modal to add or edit adventures
export const NewAdventureModal = ({ isOpen, setClosed, chosenAdventure, locations }) => {
    const classes = useStyles();
    const [name, setName] = useState('')
    const [description, setDescription] = useState('');
    const [startingLocation, setStartingLocation] = useState('');
    const [nameError, setNameError] = useState(false);
    const [descriptionError, setDescriptionError] = useState(false);
    const [startingLocationError, setStartingLocationError] = useState(false)

    // When the chosen adventure changes, update all properties. This happens when edit is clicked or the modal is closed.
    useEffect(() => {
        setName(chosenAdventure ? chosenAdventure.name : '');
        setDescription(chosenAdventure ? chosenAdventure.description : '');
        setStartingLocation(chosenAdventure ? chosenAdventure.startingLocation : '');
    }, [chosenAdventure])

    // Making sure input is valid before submitting
    const validateValues = () => {
        let isValid = true;
        if (_.isEmpty(name)) {
            isValid = false;
            setNameError(_.isEmpty(true));
        } else {
            setNameError(false)
        }

        if (_.isEmpty(description)) {
            isValid = false;
            setDescriptionError(_.isEmpty(true));
        } else {
            setDescriptionError(false)
        }

        if (!startingLocation) {
            isValid = false;
            setStartingLocationError(true);
        } else {
            setStartingLocationError(false);
        }

        return isValid;
    }

    // When creating a new adventure we send a POST request to the locations api, with all the data in the body
    const createNewAdventure = async body => {
        const request = new Request('api/adventures', { body, method: 'POST', headers: { "content-type": 'application/json' } });

        const res = await fetch(request)
        if (res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    // When editing an adventure we send all the data in the body but the id, which is sent as a query.
    const editAdventure = async body => {
        const request = new Request(`api/adventures?id=${chosenAdventure.id}`, { body, method: 'PUT', headers: { "content-type": 'application/json' } });

        const res = await fetch(request)
        if (res.ok) {
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
            startingLocation: Number(startingLocation)
        });

        chosenAdventure ? editAdventure(body) : createNewAdventure(body);
    }

    const handleSelectedTypeChange = (event) => {
        setStartingLocation(event.target.value);
    };

    return (
        <Modal open={isOpen} className={classes.modal} onClose={setClosed}>
            <Paper className={classes.paper}>
                <TextField label="Adventure Name" value={name} error={!!nameError} onChange={e => setName(e.target.value)} className={classes.textField} />
                <TextField label="Adventure Description" value={description} error={!!descriptionError} onChange={e => setDescription(e.target.value)} className={classes.textField} />
                <Select
                    value={startingLocation}
                    onChange={handleSelectedTypeChange}
                    displayEmpty
                    className={classes.select}
                    error={startingLocationError}
                >
                    <MenuItem value="">
                        <em>None</em>
                    </MenuItem>
                    {locations.map(type => <MenuItem key={type.id} value={type.id}>{type.name}</MenuItem>)}
                </Select>
                <Button className={classes.submitButton} color='primary' variant='contained' onClick={onSubmit}>{chosenAdventure ? "Edit" : "Add"}</Button>
            </Paper>
        </Modal>
    )
}