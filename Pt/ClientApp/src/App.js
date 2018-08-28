import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Savings } from './components/Savings'
import { Super } from './components/Super'
import { SavingDetail } from './components/savingDetail'

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
        <Route path='/savings' component={Savings} />
        <Route path='/super' component={Super} />
        <Route path='/savingDetail/:id' component={SavingDetail} />
      </Layout>
    );
  }
}
