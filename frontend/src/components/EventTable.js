import React from 'react';
import PropTypes from 'prop-types';

const EventTable = ({ events, rightColumnLabel }) => {
  // Create a formatter for numbers with commas and two decimal places
  const currencyFormatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });

  return (
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
            <td>
              {rightColumnLabel === 'Total Revenue'
                ? currencyFormatter.format(event.location / 100) // Format cents to dollars with commas
                : event.location} {/* Display as-is for tickets sold */}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

EventTable.propTypes = {
  events: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired, // Allow string or number for ID
      name: PropTypes.string.isRequired,
      startsOn: PropTypes.string.isRequired,
      endsOn: PropTypes.string.isRequired,
      location: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired, // Allow string or number for location
    })
  ).isRequired,
  rightColumnLabel: PropTypes.string.isRequired, // Label for the rightmost column
};

export default EventTable;