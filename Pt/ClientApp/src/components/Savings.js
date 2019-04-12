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
            <th><p className="text-center">Name</p></th>
            <th><p className="text-center">Type</p></th>
            <th><p className="text-center">Amount Invested</p></th>
            <th><p className="text-center">Qty</p></th>
            <th><p className="text-center">Cmp</p></th>
            <th><p className="text-center">Latest Amount</p></th>
            <th><p className="text-center">P/L</p></th>
            <th><p className="text-center">P/L %</p></th>
            <th><p className="text-center">Today%</p></th>
            <th><p className="text-center">5</p></th>
            <th><p className="text-center">20</p></th>                      
            <th><p className="text-center">100</p></th>            
          </tr>
        </thead>
        <tbody>
                  {
                      savings.map(item =>
                          <tr key={item.id}>
                              <td><p className="text-right" title={item.name}><Link to={`/savingDetail/${item.code}`}>{item.name.substring(0, 10)}</Link></p></td>
                              <td><p className="text-right">{item.type.substring(0,2)}</p></td>
                              <td><p className="text-right">{item.amountInvested}</p></td>
                              <td><p className="text-right">{item.qty}</p></td>
                              <td><p className="text-right">{item.cmpHistoryDesc[0]}</p></td>
                              <td><p className="text-right">{item.amount}</p></td>
                              <td className="text-right" className={item.amount - item.amountInvested >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}><p>{(item.amount - item.amountInvested).toFixed(2)}</p></td>
                              <td className="text-right" className={((item.amount - item.amountInvested) * 100 / item.amountInvested) >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}><p>{((item.amount - item.amountInvested) * 100 / item.amountInvested).toFixed(2)}%</p></td>
                              <td className={item.mostRecentPct >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}>
                                  <Sparklines data={item.cmpHistory} limit={2} width={100} height={20} margin={5}><SparklinesLine /></Sparklines>
                                  {item.mostRecentPct}%
                              </td>
                              <td className={item.last5DaysMpPct >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}>
                                  <Sparklines data={item.cmpHistory} limit={5} width={100} height={20} margin={5}><SparklinesLine /></Sparklines>
                                  {item.last5DaysPct}%&nbsp;&nbsp;{item.last5DaysMpPct}%
                              </td>
                              <td className={item.last20DaysMpPct >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}>
                                  <Sparklines data={item.cmpHistory} limit={20} width={100} height={20} margin={5}><SparklinesLine /></Sparklines>
                                  {item.last5DaysPct}%&nbsp;&nbsp;{item.last20DaysMpPct}%
                              </td> 
                              <td className={item.last100DaysMpPct >= 0 ? 'text-right bg-success' : 'text-right bg-danger'}>
                                  <Sparklines data={item.cmpHistory} limit={100} width={100} height={20} margin={5}><SparklinesLine /></Sparklines>
                                  {item.last100DaysPct}%&nbsp;&nbsp;{item.last100DaysMpPct}%
                              </td>
                          </tr>
                      )}
                  
        </tbody>
          </table>          
      );
  }

  render() {

    let savingsWithoutBankAccounts = [];
      

    let totalCv = 0, totalCmf = 0, totalCs = 0, totalCsv = 0;
    let totalAv = 0, totalAmf = 0, totalAs = 0, totalAsv = 0;

    let id; 
    this.state.savings.map(item => {
        id = item.code;
        console.log(id);
        if (item.type === "Stocks") {
            totalCs += parseInt(item.amount);
            totalAs += parseInt(item.amountInvested);
            savingsWithoutBankAccounts.push(item);
        }
        else if (item.type === "Mutual Funds") {
            totalCmf += parseInt(item.amount);
            totalAmf += parseInt(item.amountInvested);
            savingsWithoutBankAccounts.push(item);
        }
        else if (item.type === "Savings") {
            totalCsv += parseInt(item.amount);
            totalAsv += parseInt(item.amountInvested);
        }        
    });

    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Savings.renderSavingsTable(savingsWithoutBankAccounts);
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
                        <td colspan="2"><p className="text-center" className={totalCv - totalAv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{((totalCv - totalAv) * 100 / totalAv).toFixed(2)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCmf - totalAmf >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{((totalCmf - totalAmf) * 100 / totalAmf).toFixed(2)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCs - totalAs >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{((totalCs - totalAs) * 100 / totalAs).toFixed(2)}%</p></td>
                        <td colspan="2"><p className="text-center" className={totalCsv - totalAsv >= 0 ? 'text-center bg-success' : 'text-center bg-danger'}>{((totalCsv - totalAsv) * 100 / totalAsv).toFixed(2)}%</p></td>
                    </tr>
                </tbody>
            </table>
            <p>&nbsp;</p>
            {contents}            
        </div>
    )
  }
}
