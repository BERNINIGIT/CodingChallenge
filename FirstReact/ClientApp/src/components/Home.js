import axios from 'axios';
import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { loading: false, contents: <div></div>, selectedFile: null, popularCar: ''};
    }   

    onFileChange = event => {
        this.setState({ selectedFile: event.target.files[0] });
    };

    onFileUpload = () => {
        if (this.state.selectedFile == null)
            return;
        const formData = new FormData();

        formData.append(
            "formFile",
            this.state.selectedFile
        );
        axios.post("file", formData).then(res => {
            if (res.status == 200) {
                this.setState({ popularCar: this.getMostRepeatedfunction(res.data.map((i) => i.vehicle)) });
                this.setState({ contents: Home.renderTable(res.data, this.state.popularCar) });
            }
            else {
                this.setState({ contents: Home.showError(res.statusText) }); 
            }
        }).catch(error => {
            var message = '';
            if (error.response.data.detail)
                message = error.response.data.detail;
            else
                message = error.response.data;
            this.setState({ contents: Home.showError(message) });
        });
    };

    getMostRepeatedfunction = (array) => {
        if (array.length == 0)
            return null;
        var items = {};
        var maxItem = array[0], maxCount = 1;
        for (var i = 0; i < array.length; i++) {
            if (items[array[i]] == null)
                items[array[i]] = 1;
            else
                items[array[i]]++;
            if (items[array[i]] > maxCount) {
                maxItem = array[i];
                maxCount = items[array[i]];
            }
        }
        return maxItem;
    }

    static renderTable(carInfo, popularCar) {
        return (
            <div>            
                <h1 id="tabelLabel" >File data</h1>
                <span><h3>Most popular car is: {popularCar}</h3></span>                    
                
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Deal Number</th>
                            <th>Customer Name</th>
                            <th>Dealership Name</th>
                            <th>Vehicle</th>
                            <th>Price</th>
                            <th>Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {carInfo.map(carSale =>
                            <tr key={carSale.dealNumber}>
                                <td>{carSale.dealNumber}</td>
                                <td>{carSale.customerName}</td>
                                <td>{carSale.dealershipName}</td>
                                <td>{carSale.vehicle}</td>
                                <td><span>CAD$</span>{carSale.price}</td>
                                <td>{carSale.date}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
    static showError(message) {
        return (
            <div>
                <span>{message}</span>
            </div>
        );
    }
    render() {
        return (
            <div>
                <h1 id="tabelLabel" >Car Sales</h1>
                <div>
                    <input type="file" onChange={this.onFileChange} />
                    <button onClick={this.onFileUpload}>
                        Upload CSV file
                    </button>
                </div>
                <div>
                    {this.state.contents}
                </div>
            </div>
        );
    }
}
