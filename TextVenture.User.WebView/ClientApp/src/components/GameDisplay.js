import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Typography, makeStyles, Card, Paper } from '@material-ui/core';
import { NavLink, Button } from 'reactstrap';
import _, { update } from 'lodash';

const baseButtonStyle = {
    maxWidth: 'fit-content',
    backgroundColor: '#c6c7c7',
    borderRadius: '10px',
    minWidth: '15%',
    textAlign: 'center',
    marginBottom: '1%'
}

const useStyles = makeStyles(
    {
        buttonsContainer: {
            alignContent: 'center',
            marginLeft: '35%',
            marginTop: '35%',
            position: 'sticky'
        },
        blockingButton: {
            display: 'block',
            marginLeft: '10%',
            ...baseButtonStyle
        },
        nonBlockingButton: {
            marginRight: '5%',
            display: 'inline-block',
            ...baseButtonStyle
        },
        card: {
            maxWidth: '20%',
            minWidth: '15%',
            marginLeft: '10%',
            marginTop: '5%',
            height: '20%',
            backgroundColor: '#c2cbd2',
            display: 'inline-block'
        }
    }
)
// Missing player display.
export const GameDisplay = ({ match }) => {
    const { id } = match.params;
    const [location, setLocation] = useState({});
    const [enemy, setEnemy] = useState({});
    const [item, setItem] = useState({});
    const [player, setPlayer] = useState({
        health: 100,
        items: [],
        baseAttack: 5
    })
    const [visitedLocations, setVisitedLocations] = useState({});
    const classes = useStyles();

    useEffect(() => {
        setEnemy({});
        setItem({});
        getLocationDetails();
    }, [match.params.id]);

    useEffect(() => {
        if (_.isEmpty(location)) return;
        const visitedThisLocation = visitedLocations[match.params.id];
        if (!visitedThisLocation) {
            updateVisitedLocations(false, false);
        }
        // If the enemy has been beaten we don't want to fetch it
        if (location && location.enemy && (!visitedThisLocation || !visitedThisLocation.enemyBeaten)) {
            getEnemy(location.enemy);
        }
        // If the item has been taken we don't want to fetch it.
        if (location && location.item && (!visitedThisLocation || !visitedThisLocation.itemTaken)) {
            getItem(location.item);
        }

    }, [location])

    const getLocationDetails = async () => {
        const result = await fetch(`api/locations/${id}`);
        const locationJson = await result.json();
        setLocation(locationJson);
    }

    const getEnemy = async enemyId => {
        const result = await fetch(`api/enemies/${enemyId}`);
        const enemyJson = await result.json();
        setEnemy(enemyJson)
    }

    const getItem = async itemId => {
        const result = await fetch(`api/items/get/${itemId}`);
        const itemJson = await result.json();
        setItem(itemJson);
    }

    if (_.isEmpty(location)) {
        return <div>Loading...</div>
    }

    const updateVisitedLocations = (itemTaken, enemyBeaten) => {
        const visitedLocationsClone = _.cloneDeep(visitedLocations);
        visitedLocationsClone[match.params.id] = { itemTaken, enemyBeaten };
        setVisitedLocations(visitedLocationsClone);
    }

    // Monster has a range of damage. We get a random number in it.
    const getMonsterAttack = () => Math.floor(Math.random() * (enemy.maxDamage - enemy.minDamage) + enemy.minDamage);

    // Player has a base attack + all attack items
    const getPlayerAttack = () => {
        const attackItems = _.filter(player.items, item => item.itemEffect === 'Attack');
        const itemsAttack = _.sumBy(attackItems, item => item.effectLevel);
        return player.baseAttack + itemsAttack;
    }

    // Player has no base defense. Only defense are defense items
    const getPlayerDefense = () => {
        const defenseItems = _.filter(player.items, item => item.itemEffect === 'Defense');
        const itemsDefense = _.sumBy(defenseItems, item => item.effectLevel);
        return itemsDefense;
    }

    // Monster damage is reduced by player defense. If it's lower than zero we don't want the player to heal.
    const calculateMonsterDamage = () => {
        const damage = getMonsterAttack() - getPlayerDefense();
        return damage > 0 ? damage : 0;
    }

    const attackPlayer = () => {
        const updatedPlayer = _.clone(player);
        updatedPlayer.health = updatedPlayer.health - calculateMonsterDamage();

        if (updatedPlayer.health <= 0) {
            window.alert('You died!');
            window.location.pathname = '/';
        }
        setPlayer(updatedPlayer);
    }

    // When attacking a monster, if the monster survives it attacks back.
    const attackMonster = () => {
        const updatedEnemy = _.clone(enemy);
        updatedEnemy.health = updatedEnemy.health - getPlayerAttack();
        setEnemy(updatedEnemy);

        if (updatedEnemy.health > 0) {
            attackPlayer();
        } else {
            updateVisitedLocations(visitedLocations[match.params.id].itemTaken, true)
            setEnemy({});
        }
    }

    // Taking an item and updating visited locations.
    const takeItem = () => {
        const updatedPlayer = _.clone(player);
        updatedPlayer.items.push(item);
        setPlayer(updatedPlayer);
        setItem({});
        updateVisitedLocations(true, visitedLocations[match.params.id].enemyBeaten)
    }

    const usePotion = () => {
        const updatedPlayer = _.cloneDeep(player);
        const potion = _.find(updatedPlayer.items, item => item.itemEffect === 'Health');
        if (potion) {
            // Take the potion out of the bag and heal the player.
            updatedPlayer.items = _.filter(updatedPlayer.items, item => item.id !== potion.id)
            updatedPlayer.health = updatedPlayer.health + potion.effectLevel;
            setPlayer(updatedPlayer);
        }
    }

    const isInBattle = !!enemy.id;

    return (
        <div>
            <Paper>
                <Typography variant='h2' component='h2'>{location.name}</Typography>
                <Typography variant='body1' component='p'>{location.description}</Typography>
        
                <Card className={classes.card} variant='outlined'>
                    <Typography variant='h3' component='h3'>Player</Typography>
                    <Typography variant='body1' component='p'>Health: {player.health}</Typography>
                    <ul>
                        {_.map(player.items, item => <li>{item.name}</li>)}
                    </ul>
                </Card>
                
                <Card className={classes.card} variant='outlined'>
                    <Typography variant='h3' component='h3'>{!item.id && 'No'} Item</Typography>
                    <Typography variant='h4' component='h4'>{item.name}</Typography>
                    <Typography variant='body1' component='p'>Power: {item.effectLevel}</Typography>
                </Card>

                <Card className={classes.card} variant='outlined'>
                    <Typography variant='h3' component='h3'>{!enemy.id && 'No'} Enemy</Typography>
                    <Typography variant='h4' component='h4'>{enemy.name}</Typography>
                    <Typography variant='body1' component='p'>Health: {enemy.health}</Typography>
                </Card>
            </Paper>

            <div className={classes.buttonsContainer}>
                <Button className={classes.nonBlockingButton} onClick={enemy.id ? attackMonster : takeItem}>{enemy.id ? "Attack" : "Take Item"}</Button>
                <Button className={classes.nonBlockingButton} onClick={usePotion}>Use potion</Button>
                <NavLink tag={Link} to={`/game/${location.north}`} disabled={!location || !location.north || isInBattle} className={classes.blockingButton}>North</NavLink>
                <NavLink tag={Link} to={`/game/${location.west}`} disabled={!location || !location.west || isInBattle} className={classes.nonBlockingButton}>West</NavLink>
                <NavLink tag={Link} to={`/game/${location.east}`} disabled={!location || !location.east || isInBattle} className={classes.nonBlockingButton}>East</NavLink>
                <NavLink tag={Link} to={`/game/${location.south}`} disabled={!location || !location.south || isInBattle} className={classes.blockingButton}>South</NavLink>
            </div>
        </div>
    )
}