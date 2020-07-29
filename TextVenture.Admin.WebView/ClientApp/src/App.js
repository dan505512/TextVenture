import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { EnemiesTable } from './Enemies/EnemiesList';
import { ItemsTable } from './Items/ItemsList';
import { LocationsTable } from './Locations/LocationsList'
import { AdventuresTable } from './Adventures/AdventuresList';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/enemyList' component={EnemiesTable} />
        <Route path='/itemList' component={ItemsTable} />
        <Route path='/locationList' component={LocationsTable} />
        <Route path='/adventureList' component={AdventuresTable} />
      </Layout>
    );
  }
}
