import React from "react";
import { useState, useEffect } from "react";
import { Button } from "@material-ui/core";
import { NewItemModal } from "./NewItemModal";

/// A table containing all the adventures. Enables addition and edit.
export const ItemsTable = () => {
    const [items, setItems] = useState([]);
    const [newItemModalOpen, setNewItemModalOpen] = useState(false);
    const [chosenItem, setChosenItem] = useState(null);

    // Getting all the items from the api
    const getItems = async () => {
        const response = await fetch('api/items/get');
        const data = await response.json();
        setItems(data);
    }

    useEffect(() => {
        getItems();
    }, [])

    // Closing the modal and making sure no item stays selected
    const closeModal = () => {
      setNewItemModalOpen(false);
      setChosenItem(null);
    }

    // When editing trasfers the relevant data down to the modal to edit the right item
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