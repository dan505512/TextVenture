import React, { useState, useEffect } from 'react';
import { Modal, TextField, Paper, Button } from '@material-ui/core';
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
    }
}));

export const NewEnemyModal = ({ isOpen, setClosed, chosenEnemy }) => {
    const classes = useStyles();
    const [name, setName] = useState(chosenEnemy ? chosenEnemy.name : '')
    const [health, setHealth] = useState(chosenEnemy ? chosenEnemy.health : '')
    const [min, setMin] = useState(chosenEnemy ? chosenEnemy.minDamage : '')
    const [max, setMax] = useState(chosenEnemy ? chosenEnemy.maxDamage : '')
    const [nameError, setNameError] = useState(false)
    const [healthError, setHealthError] = useState(false)
    const [minError, setMinError] = useState(false)
    const [maxError, setMaxError] = useState(false)

    // When the chosen enemy changes, update all properties. This happens when edit is clicked or the modal is closed.
    useEffect(() => {
        setName(chosenEnemy ? chosenEnemy.name : '');
        setHealth(chosenEnemy ? chosenEnemy.health : '');
        setMin(chosenEnemy ? chosenEnemy.minDamage : '');
        setMax(chosenEnemy ? chosenEnemy.maxDamage : '');
    }, [chosenEnemy])

    // Making sure input is valid before submitting
    const validateValues = () => {
        let isValid = true;
        if (_.isEmpty(name)) {
            isValid = false;
            setNameError(_.isEmpty(true));
        } else {
            setNameError(false)
        }

        if (isNaN(health)) {
            isValid = false;
            setHealthError(true);
        } else {
            setHealthError(false);
        }

        if (isNaN(min)) {
            isValid = false;
            setMinError(true);
        } else {
            setMinError(false);
        }

        if (isNaN(max)) {
            isValid = false;
            setMaxError(true);
        } else {
            setMaxError(false);
        }

        return isValid;
    }

    // When creating a new enemy we send a POST request to the enemies api, with all the data in the body
    const createNewEnemy = async body => {
        const request = new Request('api/enemies', { body, method: 'POST', headers: { "content-type": 'application/json' } });

        const res = await fetch(request)
        if (res.ok) {
            window.location.reload();
        } else {
            console.error(request);
            window.alert("Failed to submit")
        }
    }

    // When editing an enemy we send all the data in the body but the id, which is sent as a query.
    const editEnemy = async body => {
        const request = new Request(`api/enemies?id=${chosenEnemy.id}`, { body, method: 'PUT', headers: { "content-type": 'application/json' } });

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
            health: Number(health),
            minDamage: Number(min),
            maxDamage: Number(max)
        });

        chosenEnemy ? editEnemy(body) : createNewEnemy(body);
    }

    return (
        <Modal open={isOpen} className={classes.modal} onClose={setClosed}>
            <Paper className={classes.paper}>
                <TextField label="Enemy Name" value={name} error={!!nameError} onChange={e => setName(e.target.value)} className={classes.textField} />
                <TextField label="Enemy Health" value={health} error={!!healthError} onChange={e => setHealth(e.target.value)} className={classes.textField} />
                <TextField label="Enemy Min Damage" value={min} error={!!minError} onChange={e => setMin(e.target.value)} className={classes.textField} />
                <TextField label="Enemy Max Damage" value={max} error={!!maxError} onChange={e => setMax(e.target.value)} className={classes.textField} />
                <Button className={classes.submitButton} color='primary' variant='contained' onClick={onSubmit}>{chosenEnemy ? "Edit" : "Add"}</Button>
            </Paper>
        </Modal>
    )
}