/**
 * Sorting optimized for performance by caching parsed dates and reducing redundant computations.
 **/

const sortEvents = (events, sortBy, sortOrder) => {
  
  // 1 if ascending order, -1 if descending
  const sortOrderMultiplier = sortOrder === 'asc' ? 1 : -1;

  // Create shallow copy of the events array
  return [...events].sort((a, b) => {

    let comparison = 0;

    if (sortBy === 'name') {
      comparison = sortOrderMultiplier * a.name.localeCompare(b.name);
    } 
    else if (sortBy === 'startDate') {
      // Parse 'startsOn' into a timestamp
      // Cache parsed date to a temp for reuse to avoid redundant parsing
      const dateA = a._parsedStartDate || (a._parsedStartDate = new Date(a.startsOn).getTime());
      const dateB = b._parsedStartDate || (b._parsedStartDate = new Date(b.startsOn).getTime());

      // Subtract the two timestamps to determine the chronological order and multiply for asc or desc
      comparison = sortOrderMultiplier * (dateA - dateB);
    }

    // Return the comparison result, 0 means equal
    return comparison;
  });
};

export default sortEvents;