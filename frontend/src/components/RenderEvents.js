import React from 'react';
import EventCard from './EventCard';
import EventTable from './EventTable';

const RenderEvents = ({ sortedEvents, isNarrowScreen }) => {
  return isNarrowScreen ? (
    <div className="row">
      {sortedEvents.map((event) => (
        <EventCard key={event.id} event={event} />
      ))}
    </div>
  ) : (
    <EventTable events={sortedEvents} />
  );
};

export default RenderEvents;