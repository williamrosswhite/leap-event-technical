import React from 'react';
import PropTypes from 'prop-types';

const EventTable = ({ events, rightColumnLabel }) => (
  <table className="table table-bordered">
    <thead className="thead-light">
      <tr>
        <th>Event ID</th>
        <th>Event Name</th>
        <th>Start Date</th>
        <th>End Date</th>
        <th>{rightColumnLabel}</th> {/* Dynamic column title */}
      </tr>
    </thead>
    <tbody>
      {events.map((event) => (
        <tr key={event.id}>
          <td>{event.id}</td>
          <td>{event.name}</td>
          <td>{new Date(event.startsOn).toLocaleString()}</td>
          <td>{new Date(event.endsOn).toLocaleString()}</td>
          <td>{event.location}</td> {/* Rightmost column value */}
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
  rightColumnLabel: PropTypes.string.isRequired, // Label for the rightmost column
};

export default EventTable;