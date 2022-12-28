function sortBooks(books, column, ascending) {
  console.log("Sort books from utils", column, ascending);
  const booksCopy = [...books];
  if (column === "Title") {
    booksCopy.sort((a, b) => {
      if (a.Title.toLowerCase() > b.Title.toLowerCase())
        return ascending ? 1 : -1;
      if (a.Title.toLowerCase() < b.Title.toLowerCase())
        return ascending ? -1 : 1;
      return 0;
    });
  } else if (column === "AverageRating") {
    booksCopy.sort((a, b) => {
      if (a.AverageRating === null) return 1;
      if (b.AverageRating === null) return -1;
      if (a.AverageRating > b.AverageRating) return ascending ? 1 : -1;
      if (a.AverageRating < b.AverageRating) return ascending ? -1 : 1;
      return 0;
    });
  } else if (column === "ReleaseYear") {
    booksCopy.sort((a, b) => {
      if (a.ReleaseYear === null) return 1;
      if (b.ReleaseYear === null) return -1;
      if (a.ReleaseYear > b.ReleaseYear) return ascending ? 1 : -1;
      if (a.ReleaseYear < b.ReleaseYear) return ascending ? -1 : 1;
      return 0;
    });
  }
  console.log(booksCopy);
  return booksCopy;
}

module.exports = {
  sortBooks
};
