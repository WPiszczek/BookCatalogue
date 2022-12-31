function sortAuthors(authors, column, ascending) {
  console.log("Sort authors from utils", column, ascending);
  const authorsCopy = [...authors];
  if (column === "Name") {
    authorsCopy.sort((a, b) => {
      if (a.Name.toLowerCase() > b.Name.toLowerCase())
        return ascending ? 1 : -1;
      if (a.Name.toLowerCase() < b.Name.toLowerCase())
        return ascending ? -1 : 1;
      return 0;
    });
  } else if (column === "AverageRating") {
    authorsCopy.sort((a, b) => {
      if (a.AverageRating === null) return 1;
      if (b.AverageRating === null) return -1;
      if (a.AverageRating > b.AverageRating) return ascending ? 1 : -1;
      if (a.AverageRating < b.AverageRating) return ascending ? -1 : 1;
      return 0;
    });
  }
  console.log(authorsCopy);
  return authorsCopy;
}

module.exports = {
  sortAuthors
};
