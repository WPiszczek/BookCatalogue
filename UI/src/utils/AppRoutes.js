import AuthorsPage from "../authors/AuthorsPage";
import BooksPage from "../books/BooksPage";
import ReviewsPage from "../reviews/ReviewsPage";

const AppRoutes = [
  {
    path: "/books",
    element: <BooksPage />,
    name: "Books",
  },
  {
    path: "/authors",
    element: <AuthorsPage />,
    name: "Authors",
  },
  {
    path: "/reviews",
    element: <ReviewsPage />,
    name: "Reviews",
  },
];

export default AppRoutes;
