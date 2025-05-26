import React from 'react';
import PropTypes from 'prop-types';
import EventCard from './EventCard';
import EventTable from './EventTable';

const RenderEvents = ({ sortedEvents, isNarrowScreen, rightColumnLabel }) => {
  return isNarrowScreen ? (
    <div className="row">
      {sortedEvents.map((event) => (
        <EventCard key={event.id} event={event} />
      ))}
    </div>
  ) : (
    <EventTable events={sortedEvents} rightColumnLabel={rightColumnLabel} />
  );
};

RenderEvents.propTypes = {
  sortedEvents: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      startsOn: PropTypes.string.isRequired,
      endsOn: PropTypes.string.isRequired,
      location: PropTypes.string.isRequired,
    })
  ).isRequired,
  isNarrowScreen: PropTypes.bool.isRequired,
  rightColumnLabel: PropTypes.string.isRequired, // Add prop type for the column label
};

export default RenderEvents;