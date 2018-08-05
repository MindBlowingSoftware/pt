import React, { Component } from 'react';
import { Sparklines, SparklinesLine } from 'react-sparklines';

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

  static renderDetailsRow() {
      <tr>
          <td colspan="8">
              Details
          </td>
      </tr>
  }

  static renderSavingsTable(self,savings) {
      
      var i = 1;
      return (
      <table className='table'>
        <thead>
          <tr>
            <th><p className="text-right">Name</p></th>
            <th><p className="text-right">Type</p></th>
            <th><p className="text-right">Amount Invested</p></th>
            <th><p className="text-right">Latest Amount</p></th>
            <th><p className="text-right">P/L</p></th>
            <th><p className="text-right">P/L %</p></th>
            <th><p className="text-right">Latest Amount Date</p></th>
            <th><p className="text-right">Sparkline</p></th>                      
          </tr>
        </thead>
        <tbody>
                  {
                      savings.map(item =>
                          <tr key={item.id} className={item.amount - item.amountInvested >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}>
                              <td><p className="text-right" title={item.name}>{item.name.substring(0, 10)}</p></td>
                              <td><p className="text-right">{item.type}</p></td>
                              <td><p className="text-right">{item.amountInvested}</p></td>
                              <td><p className="text-right">{item.amount}</p></td>
                              <td><p className="text-right">{Math.round(item.amount - item.amountInvested)}</p></td>
                              <td><p className="text-right">{Math.round((item.amount - item.amountInvested) * 100 / item.amountInvested)}%</p></td>
                              <td><p className="text-right">{item.date}</p></td>
                              <td>
                                  <Sparklines data={item.amountHistory} limit={5} width={100} height={20} margin={5}><SparklinesLine /></Sparklines>
                              </td>                              
                          </tr>
                         
                      )}
                  
        </tbody>
          </table>          
      );
  }
  handleClick() {
      console.log("hi")
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
          : Savings.renderSavingsTable(this,this.state.savings);
    let totalCv = 0,totalAi = 0;
    
    this.state.savings.map(item => {
        totalCv += parseInt(item.amount);
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
