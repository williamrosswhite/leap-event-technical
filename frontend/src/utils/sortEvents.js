/**
 * Sorts an array of event objects based on a specified criterion and order.
 * Optimized for high performance by caching parsed dates and reducing redundant computations.
 *
 * @param {Array} events - The array of event objects to be sorted.
 * @param {string} sortBy - The property to sort by. Can be 'name' or 'startDate'.
 * @param {string} sortOrder - The sorting order. Can be 'asc' for ascending or 'desc' for descending.
 * @returns {Array} - A new array of sorted event objects.
 */
const sortEvents = (events, sortBy, sortOrder) => {
  // Determine the multiplier for sorting order.
  // If sortOrder is 'asc', multiplier is 1 (normal sorting).
  // If sortOrder is 'desc', multiplier is -1 (reverses the sorting order).
  const multiplier = sortOrder === 'asc' ? 1 : -1;

  // Create a shallow copy of the events array to avoid mutating the original array.
  // This ensures the function adheres to functional programming principles.
  return [...events].sort((a, b) => {
    // Initialize a variable to hold the comparison result.
    let comparison = 0;

    // Handle sorting by 'name'.
    if (sortBy === 'name') {
      // Use localeCompare for string comparison to handle case sensitivity and locale-specific sorting.
      // Multiply the result by the multiplier to adjust for ascending or descending order.
      comparison = multiplier * a.name.localeCompare(b.name);
    } 
    // Handle sorting by 'startDate'.
    else if (sortBy === 'startDate') {
      // Parse the 'startsOn' property into a timestamp (milliseconds since epoch).
      // Cache the parsed date in a temporary property (_parsedStartDate) to avoid redundant parsing.
      // If the date is already cached, reuse it; otherwise, parse and cache it.
      const dateA = a._parsedStartDate || (a._parsedStartDate = new Date(a.startsOn).getTime());
      const dateB = b._parsedStartDate || (b._parsedStartDate = new Date(b.startsOn).getTime());

      // Subtract the two timestamps to determine the chronological order.
      // Multiply the result by the multiplier to adjust for ascending or descending order.
      comparison = multiplier * (dateA - dateB);
    }

    // Return the comparison result.
    // If comparison is 0, the two items are considered equal for sorting purposes.
    return comparison;
  });
};

export default sortEvents;


// const sortEvents = (events, sortBy, sortOrder) => {
//   const multiplier = sortOrder === 'asc' ? 1 : -1;

//   return [...events].sort((a, b) => {
//     if (sortBy === 'name') {
//       return multiplier * a.name.localeCompare(b.name);
//     } else if (sortBy === 'startDate') {
//       const dateA = a._parsedStartDate || (a._parsedStartDate = new Date(a.startsOn).getTime());
//       const dateB = b._parsedStartDate || (b._parsedStartDate = new Date(b.startsOn).getTime());
//       return multiplier * (dateA - dateB);
//     }
//     return 0;
//   });
// };

// export default sortEvents;