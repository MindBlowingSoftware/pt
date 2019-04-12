import React, { Component } from 'react';
import { Line, Bar, Linebar } from 'react-chartjs-2';
//import Chart from 'chart'
//import { Chart, Axis, Series, Tooltip, Cursor, Line } from "react-charts";


export class SavingDetail extends Component {
    displayName = SavingDetail.name

  constructor(props) {
    super(props);
    this.state = {
        saving: {}, loading: true
    };

    if (props.match.params.id !== undefined) {
        fetch('api/Portfolio/' + props.match.params.id)
            .then(response => response.json())
            .then(data => {
                this.setState({ saving: data, loading: false });
            });
       
    }  
    
  }

  static renderSavingDetail(saving) {
      
      const data = {
          // labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
          datasets: [
              {
                  type: 'line',
                  label: 'Amount Invested',
                  data: saving.amountHistory,
                  fill: false,
                  backgroundColor: '#71B37C',
                  borderColor: '#71B37C',
                  hoverBackgroundColor: '#71B37C',
                  hoverBorderColor: '#71B37C',
                  yAxisID: 'y-axis-1'
              },
              {
              label: 'Price Per Unit',
              type: 'line',
              data: saving.cmpHistory,
              fill: false,
              borderColor: '#EC932F',
              backgroundColor: '#EC932F',
              pointBorderColor: '#EC932F',
              pointBackgroundColor: '#EC932F',
              pointHoverBackgroundColor: '#EC932F',
              pointHoverBorderColor: '#EC932F',
              yAxisID: 'y-axis-2'
            },            
            //  {
            //      label: 'Amount Invested For { saving.type }',
            //    type: 'bar',
            //    data: saving.typeHistory,
            //    fill: false,
            //    borderColor: '#EC932F',
            //    backgroundColor: '#EC932F',
            //    pointBorderColor: '#EC932F',
            //    pointBackgroundColor: '#EC932F',
            //    pointHoverBackgroundColor: '#EC932F',
            //    pointHoverBorderColor: '#EC932F',
            //    yAxisID: 'y-axis-3'
            //},
          ]
      };

      const options = {
          responsive: true,
          labels: saving.amountRecordedDateHistory,
          tooltips: {
              mode: 'label'
          },
          elements: {
              line: {
                  fill: false
              }
          },
          scales: {

              xAxes: [
                  {
                      display: true,
                      gridLines: {
                          display: false
                      },

                      labels: saving.amountRecordedDateHistory,
                     
                  }
              ],
              yAxes: [
                  {
                      type: 'linear',
                      display: true,
                      position: 'left',
                      id: 'y-axis-1',
                      gridLines: {
                          display: true
                      },
                      labels: {
                          show: true,
                          
                      },
                      
                  },
                  {
                      type: 'linear',
                      display: true,
                      position: 'right',
                      id: 'y-axis-2',
                      gridLines: {
                          display: true
                      },
                      labels: {
                          show: true
                      },
                      ticks: {
                          fontColor: '#EC932F', // this here
                      }
                  },
                  //{
                  //    type: 'linear',
                  //    display: true,
                  //    position: 'right',
                  //    id: 'y-axis-3',
                  //    gridLines: {
                  //        display: false
                  //    },
                  //    labels: {
                  //        show: true
                  //    },
                  //    ticks: {
                  //        fontColor: '#EC932F', // this here
                  //    }
                  //}
              ]
          }
      };

      const plugins = [{
          afterDraw: (chartInstance, easing) => {
              const ctx = chartInstance.chart.ctx;
              //ctx.fillText("This text drawn by a plugin", 100, 100);
          }
      }];


      return (
          <div>
              <h1>{saving.name}</h1>
              <div>
                  <Bar data={data} options={options} plugins={plugins} width="600" height="250" />
              </div>
              <div>
                  
              </div>
          </div>
          
      );
  }

  render() {
      console.log(this.props.id);
      let contents = this.state.loading
          ? <p><em>Loading...</em></p>
          : SavingDetail.renderSavingDetail(this.state.saving);
      return (
          <div>
              {contents}
          </div>
      )
  }
}
