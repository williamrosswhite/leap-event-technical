import React from 'react';
import PropTypes from 'prop-types';

const EventTable = ({ events }) => (
  <table className="table table-bordered">
    <thead className="thead-light">
      <tr>
        <th>Event ID</th>
        <th>Event Name</th>
        <th>Start Date</th>
        <th>End Date</th>
        <th>Location</th>
      </tr>
    </thead>
    <tbody>
      {events.map((event) => (
        <tr key={event.id}>
          <td>{event.id}</td>
          <td>{event.name}</td>
          <td>{new Date(event.startsOn).toLocaleString()}</td>
          <td>{new Date(event.endsOn).toLocaleString()}</td>
          <td>{event.location}</td>
        </tr>
      ))}
    </tbody>
  </table>
);

EventTable.propTypes = {
  events: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      startsOn: PropTypes.string.isRequired,
      endsOn: PropTypes.string.isRequired,
      location: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default EventTable;