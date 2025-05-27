import PropTypes from 'prop-types';
import EventCard from './EventCard';

// Display sales data in either card or table format based on screen size
const SalesDataRenderer = ({ data, isNarrowScreen, cardLocationKey, renderTable }) => {
  const processedData = data.map((event) => ({
    id: event.id,
    name: event.name,
    startsOn: new Date(event.startsOn).toLocaleString(),
    endsOn: new Date(event.endsOn).toLocaleString(), 
    location: event[cardLocationKey], 
  }));

  return isNarrowScreen ? (
    <div className="row">
      {processedData.map((event) => (
        <div className="col-md-4 mb-4" key={event.id}>
          <EventCard event={event} />
        </div>
      ))}
    </div>
  ) : (
    renderTable(processedData)
  );
};

SalesDataRenderer.propTypes = {
  data: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      startsOn: PropTypes.string.isRequired,
      endsOn: PropTypes.string.isRequired,
      location: PropTypes.string,
    })
  ).isRequired,
  isNarrowScreen: PropTypes.bool.isRequired,
  cardLocationKey: PropTypes.string.isRequired,
  renderTable: PropTypes.func.isRequired,
};

export default SalesDataRenderer;