import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import { NewEnemyModal } from "./NewEnemyModal";

export const EnemiesTable = () => {
    const [enemies, setEnemies] = useState([]);
    const [newEnemyModalOpen, setNewEnemyModalOpen] = useState(false);
    const [chosenEnemy, setChosenEnemy] = useState(null);

    const FetchData = async () => {
        const response = await fetch('api/enemies');
        const data = await response.json();
        setEnemies(data);
    }

    useEffect(() => {
        FetchData();
    }, [])

    const closeModal = () => {
      setNewEnemyModalOpen(false);
      setChosenEnemy(null);
    }

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