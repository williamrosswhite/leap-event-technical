import PropTypes from 'prop-types';

const ErrorAlert = ({ message }) => (
  <div className="alert alert-danger text-center" role="alert">
    {message}
  </div>
);

ErrorAlert.propTypes = {
  message: PropTypes.string.isRequired,
};

export default ErrorAlert;