import React, { useState } from 'react';
import { TextField, Paper, makeStyles, Button } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
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
    errorP: {
        color: 'red'
    }
}));

export const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isError, setIsError] = useState(false);
    const classes = useStyles();

    
    const onSubmit = async () => {
        const request = new Request('/api/login' , {
            method: 'POST',
            body: JSON.stringify({
                username,
                password
            }),
            headers: {"content-type": 'application/json'}
        });

        const result = await fetch(request)
        const json = await result.json();
        if(!(json.statusCode === 200)) {
            setIsError(true);
            return;
        }
        
        // Becuase of .NET integration cookie's nama and value seems as null to the browser. Will be fixed in next version.
        document.cookie = json.headers[0].Value;
        window.location.pathname = '/'
    }

    return (
        <>
            <h1>Welcome! TextVenute Awaits!</h1>
            <p>Please log in so we can edit some adventures</p>
            <Paper className={classes.paper}>
                <TextField value={username} onChange={e => setUsername(e.target.value)} label='Username' className={classes.textField} />
                <TextField value={password} onChange={e => setPassword(e.target.value)} label='Password' type='password' className={classes.textField} />
                {isError && <p className={classes.errorP}>*Username or password are incorrect</p>}
                <Button variat='contained' color='primary' onClick={onSubmit}>Log in</Button>
            </Paper>
        </>)
}