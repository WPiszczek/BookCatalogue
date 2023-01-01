function sortReviews(reviews, column, ascending) {
  console.log("Sort reviews from utils", column, ascending);
  const reviewsCopy = [...reviews];
  if (column === "Rating") {
    reviewsCopy.sort((a, b) => {
      if (a.Rating === null) return 1;
      if (b.Rating === null) return -1;
      if (a.Rating > b.Rating) return ascending ? 1 : -1;
      if (a.Rating < b.Rating) return ascending ? -1 : 1;
      return 0;
    });
  } else if (column === "DateAdded") {
    reviewsCopy.sort((a, b) => {
      const _a = new Date(a.DateAdded);
      const _b = new Date(b.DateAdded);
      if (_a > _b) return ascending ? 1 : -1;
      if (_a < _b) return ascending ? -1 : 1;
      return 0;
    });
  } else {
  }
  console.log(reviewsCopy);
  return reviewsCopy;
}

module.exports = {
  sortReviews
};
