import { React, useEffect, useState } from "react";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import AuthorsFilters from "./AuthorsFilters";
import AuthorsContainer from "./AuthorsContainer";
import AddAuthorDialog from "./AddAuthorDialog";
import { sortAuthors } from "../utils/sortAuthors";

function AuthorsPage() {
  const [authors, setAuthors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showAddAuthorDialog, setShowAddAuthorDialog] = useState(false);

  useEffect(() => {
    fetchAuthorsData();
  }, []);

  async function fetchAuthorsData(name = "", status = "") {
    try {
      await axios
        .get(`/api/authors?name=${name}&status=${status}`)
        .then((response) => {
          console.log("response", response.data.Data);
          setAuthors(response.data.Data);
          setLoading(false);
        });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  function callSortAuthors(column, ascending) {
    console.log("Sort authors from page", column, ascending);
    setAuthors(sortAuthors(authors, column, ascending));
  }

  function addAuthor() {
    console.log("Add author from authors page");
    setShowAddAuthorDialog(true);
  }

  return (
    <Container>
      <h1>Authors</h1>
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          {showAddAuthorDialog && (
            <AddAuthorDialog
              show={true}
              close={() => setShowAddAuthorDialog(false)}
              fetchAuthorsData={fetchAuthorsData}
            />
          )}
          <AuthorsFilters
            fetchAuthorsData={fetchAuthorsData}
            addAuthor={addAuthor}
            sortAuthors={callSortAuthors}
          />
          {authors.length > 0 ? (
            <AuthorsContainer authors={authors} addAuthor={addAuthor} />
          ) : (
            <Container className="center margin-top">
              <i>Authors not found.</i>
            </Container>
          )}
        </>
      )}
    </Container>
  );
}

export default AuthorsPage;
