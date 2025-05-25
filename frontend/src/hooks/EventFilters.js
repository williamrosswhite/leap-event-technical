import React from 'react';
import PropTypes from 'prop-types';
import ButtonGroup from '../components/ButtonGroup';

const EventFilters = ({ days, setDays, sortBy, setSortBy, sortOrder, setSortOrder }) => (
  <>
    <div className="event-filters d-flex justify-content-center mb-3">
      <ButtonGroup
        options={[
          { value: 30, label: '30 Days' },
          { value: 60, label: '60 Days' },
          { value: 180, label: '180 Days' },
        ]}
        selected={days}
        onClick={setDays}
      />
    </div>

    <div className="event-filters d-flex justify-content-center mb-2">
      <ButtonGroup
        options={[
          { value: 'name', label: 'Sort by Name' },
          { value: 'startDate', label: 'Sort by Start Date' },
        ]}
        selected={sortBy}
        onClick={setSortBy}
      />
    </div>

    <div className="event-filters d-flex justify-content-center mb-4">
      <ButtonGroup
        options={[
          { value: 'asc', label: 'Ascending' },
          { value: 'desc', label: 'Descending' },
        ]}
        selected={sortOrder}
        onClick={setSortOrder}
      />
    </div>
  </>
);

EventFilters.propTypes = {
  days: PropTypes.number.isRequired,
  setDays: PropTypes.func.isRequired,
  sortBy: PropTypes.string.isRequired,
  setSortBy: PropTypes.func.isRequired,
  sortOrder: PropTypes.string.isRequired,
  setSortOrder: PropTypes.func.isRequired,
};

export default EventFilters;