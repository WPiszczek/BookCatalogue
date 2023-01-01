const BookCategoryMap = {
  0: "Dla dzieci",
  1: "Dla młodzieży",
  2: "Fantastyka",
  3: "Historia",
  4: "Horror",
  5: "Kryminał/Sensacja",
  6: "Kultura i sztuka",
  7: "Literatura faktu",
  8: "Literatura piękna"
};

const AuthorStatusMap = {
  0: "Active",
  1: "Dead",
  2: "Retired"
};

function getAuthorHeaderColor(status) {
  const statusString = AuthorStatusMap[status];
  if (statusString === "Active") {
    return "#198754";
  } else if (statusString === "Dead") {
    return "black";
  } else {
    return "grey";
  }
}

module.exports = {
  BookCategoryMap,
  AuthorStatusMap,
  getAuthorHeaderColor
};
