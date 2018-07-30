import React, { Component } from 'react';
//import sparkline from 'react-sparkline'

export class Savings extends Component {
  displayName = Savings.name

  constructor(props) {
    super(props);
    this.state = { savings: [], loading: true };

    fetch('api/Portfolio/Get')
      .then(response => response.json())
      .then(data => {
          this.setState({ savings: data, loading: false });
      });
  }

  static renderSavingsTable(savings) {
      console.log("renderSavingsTable");
      //var Sparkline = React.require('react-sparkline');
      var Sparkline = React.sparkline;

      var i = 1;
      return (
    
      <table className='table'>
        <thead>
          <tr>
                      <th><p className="text-right">Account</p></th>
                      <th><p className="text-right">Type</p></th>
                      <th><p className="text-right">Amount Invested</p></th>
                      <th><p className="text-right">Latest Amount</p></th>
                      <th><p className="text-right">Latest Amount Date</p></th>
                      <th><p className="text-right">Amount History</p></th>
                      <th><p className="text-right">Date History</p></th>
          </tr>
        </thead>
        <tbody>
                  {
                      savings.map(item =>
                          <tr key={i++}>
                              <td><p className="text-right">{item.name}</p></td>
                              <td><p className="text-right">{item.type}</p></td>
                              <td><p className="text-right">{item.amountInvested}</p></td>
                              <td><p className="text-right">{item.latestImportedAmount}</p></td>
                              <td><p className="text-right">{item.latestImportedAmount}</p></td>
                              <td><p className="text-right">{item.amountHistory}<Sparkline data={1,4,4,7,5,9,10}/></p></td>
                              <td><p className="text-right">{item.amountRecordedDateHistory}</p></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
          : Savings.renderSavingsTable(this.state.savings);
    let totalCv = 0,totalAi = 0;
    
    this.state.savings.map(item => {
        totalCv += parseInt(item.latestImportedAmount);
        totalAi += parseInt(item.amountInvested);
    });
    return (
      <div>
            <h1>Savings</h1>
            <h2>Current Value {totalCv}</h2>
            <h2>Amount Invested {totalAi}</h2>
        <p>&nbsp;</p>
        {contents}
      </div>
    )
  }
}
