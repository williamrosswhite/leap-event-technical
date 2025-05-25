import React from 'react';
import PropTypes from 'prop-types';

const EventCard = ({ event }) => (
  <div className="col-md-4 mb-4">
    <div className="card h-100">
      <div className="card-body">
        <h5 className="card-title">{event.name}</h5>
        <p className="card-text">
          <strong>Event ID:</strong> {event.id}
        </p>
        <p className="card-text">
          <strong>Start Date:</strong> {new Date(event.startsOn).toLocaleString()}
        </p>
        <p className="card-text">
          <strong>End Date:</strong> {new Date(event.endsOn).toLocaleString()}
        </p>
        <p className="card-text">
          <strong>Location:</strong> {event.location}
        </p>
      </div>
    </div>
  </div>
);

EventCard.propTypes = {
  event: PropTypes.shape({
    id: PropTypes.number.isRequired,
    name: PropTypes.string.isRequired,
    startsOn: PropTypes.string.isRequired,
    endsOn: PropTypes.string.isRequired,
    location: PropTypes.string.isRequired,
  }).isRequired,
};

export default EventCard;