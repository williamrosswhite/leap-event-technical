import React from 'react';

const ButtonGroup = ({ options, selected, onClick }) => (
  <div className="btn-group">
    {options.map((option) => (
      <button
        key={option.value}
        className={`btn ${selected === option.value ? 'btn-primary' : 'btn-outline-primary'}`}
        onClick={() => onClick(option.value)}
      >
        {option.label}
      </button>
    ))}
  </div>
);

export default ButtonGroup;