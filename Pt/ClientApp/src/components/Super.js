import React, { Component } from 'react';
import { SavingDetail } from './savingDetail';
import { Sparklines, SparklinesLine } from 'react-sparklines';
import { LinkContainer } from 'react-router-bootstrap';
import { Link } from 'react-router-dom';
import { Savings } from './Savings';

export class Super extends Component {
    displayName = Super.name

  constructor(props) {
    super(props);
    this.state = { savings: [], loading: true };

    fetch('api/Super/Get')
      .then(response => response.json())
      .then(data => {
          this.setState({ savings: data, loading: false });
        });
    this.render = this.render.bind(this);
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
        totalCs += parseInt(item.amount);
        totalAs += parseInt(item.amountInvested);
       
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
