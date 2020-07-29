import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import _ from 'lodash';
import { NewLocationModal } from "./NewLocationModal";

export const LocationsTable = () => {
    const [locations, setLocations] = useState([]);
    const [enemies, setEnemies] = useState([]);
    const [items, setItems] = useState([]);
    const [newLocationModalOpen, setNewLocationModalOpen] = useState(false)
    const [chosenLocation, setChosenLocation] = useState(null);

    // Fetching the items, enemies and locations.
    useEffect(() => {
        getEnemies();
        getItems();
        getLocations();
    }, [])

    const getEnemies = async () => {
        const response = await fetch('api/enemies');
        const data = await response.json();
        setEnemies(data);
    }

    const getItems = async () => {
        const response = await fetch('api/items/get');
        const data = await response.json();
        setItems(data);
    }

    const getLocations = async () => {
        const response = await fetch('api/locations');
        const data = await response.json();
        setLocations(data);
    }

    // A locations contains a lot of data from different objects. Mapping each property to the name contained in the relevant object.
    const getMappedLocationsForTable = () => {
        return _.map(locations, dbLocation => {
            const displayLocation = _.clone(dbLocation)
            if(displayLocation.north) {
                displayLocation.north = _.get(_.find(locations, l => l.id === displayLocation.north), 'name');
            }
            if(displayLocation.south) {
                displayLocation.south = _.get(_.find(locations, l => l.id === displayLocation.south), 'name');
            }
            if(displayLocation.east) {
                displayLocation.east = _.get(_.find(locations, l => l.id === displayLocation.east), 'name');
            }
            if(displayLocation.west) {
                displayLocation.west = _.get(_.find(locations, l => l.id === displayLocation.west), 'name');
            }
            if(displayLocation.item) {
                displayLocation.item = _.get(_.find(items, item => item.id === displayLocation.item), 'name');
            }
            if(displayLocation.enemy) {
                displayLocation.enemy = _.get(_.find(enemies, enemy => enemy.id === displayLocation.enemy), 'name');
            }
            return displayLocation;
        })
    }

    // Closing the modal and making sure no location stays selected
    const onModalClosed = () => {
        setNewLocationModalOpen(false);
        setChosenLocation(null);
    }

    // When editing trasfers the relevant data down to the modal to edit the right location
    const onEditClicked = locationId => {
        setChosenLocation(_.find(locations, location => locationId === location.id));
        setNewLocationModalOpen(true);
    }

    return (
    <>
        <table className='table table-striped' aria-labelledby='tabelLabel'>
        <thead>
            <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>North</th>
            <th>South</th>
            <th>East</th>
            <th>West</th>
            <th>Item</th>
            <th>Enemy</th>
            </tr>
        </thead>
        <tbody>
            {getMappedLocationsForTable().map(location =>
            <tr key={location.id}>
                <td>{location.id}</td>
                <td>{location.name}</td>
                <td>{location.description}</td>
                <td>{location.north}</td>
                <td>{location.south}</td>
                <td>{location.east}</td>
                <td>{location.west}</td>
                <td>{location.item}</td>
                <td>{location.enemy}</td>
                <td><Button variant='contained' color='secondary' onClick={() => onEditClicked(location.id)}>Edit</Button></td>
            </tr>
            )}
        </tbody>
        </table>
        <NewLocationModal isOpen={newLocationModalOpen} setClosed={(onModalClosed)} enemies={enemies} locations={locations} items={items} chosenLocation={chosenLocation} />
        <Button variant='contained' color='primary' onClick={() => setNewLocationModalOpen(true)}>Add new location</Button>
  </>);
}