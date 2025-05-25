const sortEvents = (events, sortBy, sortOrder) => {
  return [...events].sort((a, b) => {
    let comparison = 0;
    if (sortBy === 'name') {
      comparison = a.name.localeCompare(b.name);
    } else if (sortBy === 'startDate') {
      comparison = new Date(a.startsOn) - new Date(b.startsOn);
    }
    return sortOrder === 'asc' ? comparison : -comparison;
  });
};

export default sortEvents;