import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { EnemiesTable } from './Enemies/EnemiesList';
import { ItemsTable } from './Items/ItemsList';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/enemyList' component={EnemiesTable} />
        <Route path='/itemList' component={ItemsTable} />
      </Layout>
    );
  }
}
