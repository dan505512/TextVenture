import React from "react";
import { useState, useEffect } from "react";

export const EnemiesTable = () => {
    const [enemies, setEnemies] = useState([]);

    const FetchData = async () => {
        const response = await fetch('api/enemies');
        const data = await response.json();
        setEnemies(data);
    }

    useEffect(() => {
        FetchData();
    }, [])

    return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
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
              </tr>
            )}
          </tbody>
        </table>
      );
}