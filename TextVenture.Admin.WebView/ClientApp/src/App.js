import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import './custom.css'
import { Login } from './Login/Login'
import { EnemiesTable } from './Enemies/EnemiesList';
import { ItemsTable } from './Items/ItemsList';
import { LocationsTable } from './Locations/LocationsList'
import { AdventuresTable } from './Adventures/AdventuresList';

const LOGIN_PAGE = '/login';

export default () => {
  const isLoginPage = window.location.pathname === LOGIN_PAGE;
  const [loginChecked, setLoginChecked] = useState(isLoginPage);

  useEffect(() => {
    loginIfNeeded();
  })

  const loginIfNeeded = async () => {
    const result = await fetch('/api/login')
    if (!isLoginPage && result.redirected) {
      window.location.pathname = LOGIN_PAGE;
    }
    setLoginChecked(true);
  }

  if (!loginChecked)
    return (
      <h1>Logging in... Please wait</h1>
    )

  return (
    <Layout>
      <Route exact path='/' component={Home} />
      <Route exact path='/login' component={Login} />
      <Route path='/enemyList' component={EnemiesTable} />
      <Route path='/itemList' component={ItemsTable} />
      <Route path='/locationList' component={LocationsTable} />
      <Route path='/adventureList' component={AdventuresTable} />
    </Layout>
  );
}