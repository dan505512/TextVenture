import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import { NewEnemyModal } from "./NewEnemyModal";

/// A table containing all the enemies. Enables addition and edit.
export const EnemiesTable = () => {
    const [enemies, setEnemies] = useState([]);
    const [newEnemyModalOpen, setNewEnemyModalOpen] = useState(false);
    const [chosenEnemy, setChosenEnemy] = useState(null);

    // Getting all the enemies from the api
    const getEnemies = async () => {
        const response = await fetch('api/enemies');
        const data = await response.json();
        setEnemies(data);
    }

    useEffect(() => {
        getEnemies();
    }, [])

    // Closing the modal and making sure no enemy stays selected
    const closeModal = () => {
      setNewEnemyModalOpen(false);
      setChosenEnemy(null);
    }

    // When editing trasfers the relevant data down to the modal to edit the right enemy
    const onEditClicked = enemy => {
      setChosenEnemy(enemy);
      setNewEnemyModalOpen(true);
    }

    return (
      <>
        <table className='table table-striped' aria-labelledby='tabelLabel'>
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Health</th>
              <th>Min Damage</th>
              <th>Max Damage</th>
            </tr>
          </thead>
          <tbody>
            {enemies.map(enemy =>
              <tr key={enemy.id}>
                <td>{enemy.id}</td>
                <td>{enemy.name}</td>
                <td>{enemy.health}</td>
                <td>{enemy.minDamage}</td>
                <td>{enemy.maxDamage}</td>
                <td><Button variant='contained' color='secondary' onClick={() => onEditClicked(enemy)}>Edit</Button></td>
              </tr>
            )}
          </tbody>
        </table>
        <NewEnemyModal isOpen={newEnemyModalOpen} setClosed={closeModal} chosenEnemy={chosenEnemy}/>
        <Button variant='contained' color='primary' onClick={() => setNewEnemyModalOpen(true)}>Add new enemy</Button>
      </>);
}