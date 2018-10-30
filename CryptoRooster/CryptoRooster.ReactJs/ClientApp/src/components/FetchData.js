import React, { Component } from 'react';

export class FetchData extends Component {
  displayName = FetchData.name

  constructor(props) {
    super(props);
    this.state = { coins: [], loading: true };

    fetch('api/SampleData/GetCoins')
      .then(response => response.json())
      .then(data => {
        this.setState({ coins: data, loading: false });
      });
  }

  static renderForecastsTable(coins) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>Rank</th>
            <th>Name</th>
            <th>Price USD</th>
            <th>Price BTC</th>
            <th>Change 24h</th>
            </tr>
        </thead>
        <tbody>
          {coins.map(coin =>
            <tr key={coin.id}>
              <td>{coin.rank}</td>
              <td>{coin.name}</td>
              <td>{coin.price_usd}</td>
              <td>{coin.price_btc}</td>
              <td>{coin.percent_change_24h} %</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.coins);

    return (
      <div>
        <h1>Cryptocurrencies Prices</h1>
        <p>This data is based on Coin Market Cap</p>
        {contents}
      </div>
    );
  }
}
