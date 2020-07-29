import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import _ from 'lodash';
import { NewAdventureModal } from "./NewAdventureModal";

/// A table containing all the adventures. Enables addition and edit.
export const AdventuresTable = () => {
    const [locations, setLocations] = useState([]);
    const [adventures, setAdventures] = useState([]);
    const [newAdventureModalOpen, setNewAdventureModalOpen] = useState(false)
    const [chosenAdventure, setChosenAdventure] = useState(null);

    useEffect(() => {
        getLocations();
        getAdventures();
    }, [])

    // Getting all the locations from the api
    const getLocations = async () => {
        const response = await fetch('api/locations');
        const data = await response.json();
        setLocations(data);
    }

    // Getting all the adventures from the api
    const getAdventures = async () => {
        const response = await fetch('api/adventures');
        const data = await response.json();
        setAdventures(data);
    }

    // Gets the name of the relevant location according to the specific adventure's starting location
    const getStartingLocation = adventure => {
        const location = _.find(locations, location => location.id === adventure.startingLocation);
        return _.get(location, 'name');
    }

    // Closing the modal and making sure no adventure stays selected
    const onModalClosed = () => {
        setNewAdventureModalOpen(false);
        setChosenAdventure(null);
    }

    // When editing trasfers the relevant data down to the modal to edit the right adventure
    const onEditClicked = adventureId => {
        setChosenAdventure(_.find(adventures, adventure => adventureId === adventure.id));
        setNewAdventureModalOpen(true);
    }

    return (
    <>
        <table className='table table-striped' aria-labelledby='tabelLabel'>
        <thead>
            <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>Starting Location</th>
            </tr>
        </thead>
        <tbody>
            {adventures.map(adventure =>
            <tr key={adventure.id}>
                <td>{adventure.id}</td>
                <td>{adventure.name}</td>
                <td>{adventure.description}</td>
                <td>{getStartingLocation(adventure)}</td>
                <td><Button variant='contained' color='secondary' onClick={() => onEditClicked(adventure.id)}>Edit</Button></td>
            </tr>
            )}
        </tbody>
        </table>
        <NewAdventureModal isOpen={newAdventureModalOpen} setClosed={onModalClosed} chosenAdventure={chosenAdventure} locations={locations}/>
        <Button variant='contained' color='primary' onClick={() => setNewAdventureModalOpen(true)}>Add new adventure</Button>
  </>);
}