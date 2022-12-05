import axios from 'axios';
import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';

function FetchData() {
  const [forecasts, setForecasts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    populateWeatherData();
  }, []);

  async function populateWeatherData() {
    await axios.get('weatherforecast').then((response) => {
      console.log('response', response.data);
      setForecasts(response.data);
      setLoading(false);
    });
  }

  function renderForecastsTable(forecasts) {
    return (
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  }

  const contents = loading ? (
    <p>
      <em>Loading...</em>
    </p>
  ) : (
    renderForecastsTable(forecasts)
  );

  return (
    <div>
      <h1 id='tabelLabel'>Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );
}

export default FetchData;
