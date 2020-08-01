import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import { Card, CardContent, CardActions, makeStyles, Typography, Paper } from '@material-ui/core';
import _ from 'lodash';

const useStyles = makeStyles({
  card: {
    minWidth: 275,
    marginBottom: '2%'
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  title: {
    fontSize: 14,
  }
});

export const Home = () => {
  const [adventures, setAdventures] = useState([]);

  const getAdventures = async () => {
    const result = await fetch('/api/adventures');
    const adventuresJson = await result.json();
    setAdventures(adventuresJson);
  }

  useEffect(() => {
    getAdventures();
  }, []);

  const classes = useStyles();

  return (
    <div>
      <h1>Hello, adventurer!</h1>
      <p>Welcome to TextVenture</p>
      <p>Please choose an adventure you would like to play:</p>
      <Paper>
        {_.map(adventures, adventure => (
          <Card key={adventure.id} className={classes.card} variant='outlined'>
            <CardContent>
              <Typography className={classes.title} color="textSecondary" gutterBottom>
                {adventure.name}
              </Typography>
              <Typography variant="body2" component="p">
                {adventure.description}
              </Typography>
            </CardContent>
            <CardActions>
              <NavLink tag={Link} className="text-dark" to={`/game/${adventure.id}`}>I want this one!</NavLink>
            </CardActions>
          </Card>
        ))}
      </Paper>
    </div>
  );
}