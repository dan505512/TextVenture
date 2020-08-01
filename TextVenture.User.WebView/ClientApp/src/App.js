import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import './custom.css'
import { GameDisplay } from './components/GameDisplay';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/game/:id' component={GameDisplay} />
  </Layout>
);