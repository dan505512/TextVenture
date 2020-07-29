import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import { NewItemModal } from "./NewItemModal";


export const ItemsTable = () => {
    const [items, setItems] = useState([]);
    const [newItemModalOpen, setNewItemModalOpen] = useState(false);
    const [chosenItem, setChosenItem] = useState(null);

    const FetchData = async () => {
        const response = await fetch('api/items/get');
        const data = await response.json();
        setItems(data);
    }

    useEffect(() => {
        FetchData();
    }, [])

    const closeModal = () => {
      setNewItemModalOpen(false);
      setChosenItem(null);
    }

    const onEditClicked = enemy => {
      setChosenItem(enemy);
      setNewItemModalOpen(true);
    }

    return (
      <>
        <table className='table table-striped' aria-labelledby='tabelLabel'>
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Effect Level</th>
            </tr>
          </thead>
          <tbody>
            {items.map(item =>
              <tr key={item.id}>
                <td>{item.id}</td>
                <td>{item.name}</td>
                <td>{item.effectLevel}</td>
                <td><Button variant='contained' color='secondary' onClick={() => onEditClicked(item)}>Edit</Button></td>
              </tr>
            )}
          </tbody>
        </table>
        <NewItemModal isOpen={newItemModalOpen} setClosed={closeModal} chosenEnemy={chosenItem}/>
        <Button variant='contained' color='primary' onClick={() => setNewItemModalOpen(true)}>Add new item</Button>
      </>);
}