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

export const NewItemModal = ({isOpen, setClosed, chosenEnemy: chosenItem}) => {
    const classes = useStyles();
    const [name, setName] = useState('')
    const [effectLevel, setEffectLevel] = useState('')
    const [nameError, setNameError] = useState(false)
    const [effectLevelError, setEffectLevelError] = useState(false)
    const [typeError, setTypeError] = useState(false);
    const [itemTypes, setItemTypes] = useState([]);
    const [selectedItemType, setSelectedItemType] = useState("");

    useEffect(() => {
        setName(chosenItem ? chosenItem.name : '');
        setEffectLevel(chosenItem ? chosenItem.effectLevel : '');
    }, [chosenItem])

    useEffect(() => {
        getItemTypes();
    }, [])

    const getItemTypes = async () => {
        const res = await fetch('/api/items/itemtypes')
        const types = await res.json();
        console.log(types);
        setItemTypes(types);
    }

    const validateValues = () => {
        let isValid = true;
        if (_.isEmpty(name)) {
            isValid = false;
            setNameError(_.isEmpty(true));
        } else {
            setNameError(false)
        }

        if (isNaN(effectLevel) || _.isEmpty(effectLevel)) {
            isValid = false;
            setEffectLevelError(true);
        } else {
            setEffectLevelError(false);
        }

        if (!chosenItem && isNaN(selectedItemType)) {
            isValid = false;
            setTypeError(true);
        } else {
            setTypeError(false);
        }
        
        return isValid;
    }

    const createNewItem = async body => {
        const request = new Request('api/items/add', { body, method: 'POST', headers: {"content-type": 'application/json'} });

        const res = await fetch(request)
        if(res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    const editItem = async body => {
        const request = new Request(`api/items/edit?id=${chosenItem.id}`, { body, method: 'PUT', headers: {"content-type": 'application/json'} });

        const res = await fetch(request)
        if(res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    const onSubmit = () => {
        const canSubmit = validateValues();

        if (!canSubmit) {
            return;
        }
        
        const body = JSON.stringify({
            name,
            effectLevel: Number(effectLevel),
            typeId: !chosenItem ? selectedItemType : undefined
        });

        chosenItem ? editItem(body) : createNewItem(body);
    }

    const handleSelectedTypeChange = (event) => {
        setSelectedItemType(event.target.value);
      };

    return (
        <Modal open={isOpen} className={classes.modal} onClose={setClosed}>
            <Paper className={classes.paper}>
                <TextField label="Item Name" value={name} error={!!nameError} onChange={e => setName(e.target.value)} className={classes.textField}/>
                <TextField label="Item Effect Level" value={effectLevel} error={!!effectLevelError} onChange={e => setEffectLevel(e.target.value)} className={classes.textField}/>
                { !chosenItem && <Select
                        value={selectedItemType}
                        onChange={handleSelectedTypeChange}
                        displayEmpty
                        className={classes.select}
                        error={typeError}
                    >
                    <MenuItem value="">
                        <em>None</em>
                    </MenuItem>
                    {itemTypes.map(type => <MenuItem key={type.id} value={type.id}>{type.name}</MenuItem>)}
                 </Select>
                }
                <Button className={classes.submitButton} color='primary' variant='contained' onClick={onSubmit}>Add</Button>
            </Paper>
        </Modal>
    )
}