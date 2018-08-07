import React, { Component } from 'react';
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
      return (
          <div>
              
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
