import React, { Component } from 'react';
import { SavingDetail } from './savingDetail';
import { Sparklines, SparklinesLine } from 'react-sparklines';
import { LinkContainer } from 'react-router-bootstrap';
import { Link } from 'react-router-dom';

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
    this.render = this.render.bind(this);
  }

  static renderSavingsTable(savings) {
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
                              <td><p className="text-right" title={item.name}><Link to={`/savingDetail/${item.code}`}>{item.name.substring(0, 10)}</Link></p></td>
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
          : Savings.renderSavingsTable(this.state.savings);
    let totalCv = 0, totalCmf = 0, totalCs = 0, totalCsv = 0;
    let totalAv = 0, totalAmf = 0, totalAs = 0, totalAsv = 0;

    let id; 
    this.state.savings.map(item => {
        id = item.code;
        console.log(id);
        if (item.type == "Stocks") {
            totalCs += parseInt(item.amount);
            totalAs += parseInt(item.amountInvested);
        }
        else if (item.type == "Mutual Funds") {
            totalCmf += parseInt(item.amount);
            totalAmf += parseInt(item.amountInvested);
        }
        else if (item.type == "Savings") {
            totalCsv += parseInt(item.amount);
            totalAsv += parseInt(item.amountInvested);
        }        
    });
    console.log(totalCs);
    console.log(totalCmf);
    console.log(totalCsv);
    totalCv = totalCs + totalCmf + totalCsv;
    totalAv = totalAs + totalAmf + totalAsv;
    return (
        <div>
            <table className='table'>
                <thead>
                    <tr className='bg-primary'>
                        <th><p className="text-center">Current Value</p></th>
                        <th><p className="text-center">Amount Invested</p></th>
                        <th><p className="text-center">MF Value</p></th>
                        <th><p className="text-center">MF Invested</p></th>
                        <th><p className="text-center">Stocks Value</p></th>
                        <th><p className="text-center">Stocks Invested</p></th>
                        <th><p className="text-center">Savings Value</p></th>
                        <th><p className="text-center">Savings Invested</p></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{totalCv}</p></td>
                        <td><p className="text-center">{totalAv}</p></td>
                        <td><p className="text-center" className={totalCmf - totalAmf >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{totalCmf}</p></td>
                        <td><p className="text-center">{totalAmf}</p></td>
                        <td><p className="text-center" className={totalCs - totalAs >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{totalCs}</p></td>
                        <td><p className="text-center">{totalAs}</p></td>
                        <td><p className="text-center" className={totalCsv - totalAsv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{totalCsv}</p></td>
                        <td><p className="text-center">{totalAsv}</p></td>
                    </tr>

                    <tr>
                        <td colspan="2"><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{Math.round((totalCv - totalAv) * 100 / totalAv)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{Math.round((totalCmf - totalAmf) * 100 / totalAmf)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{Math.round((totalCs - totalAs) * 100 / totalAs)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{Math.round((totalCsv - totalAsv) * 100 / totalAsv)}%</p></td>
                    </tr>
                </tbody>
            </table>
            <p>&nbsp;</p>
            {contents}            
        </div>
    )
  }
}
