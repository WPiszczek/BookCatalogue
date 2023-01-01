import { React } from "react";
import { Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import AddAuthorCard from "./AddAuthorCard";
import AuthorCard from "./AuthorCard";

function AuthorsContainer(props) {
  const navigate = useNavigate();

  const redirectToAuthor = (authorId) => {
    navigate(`/authors/${authorId}`);
  };

  const authors = props.authors.map((author) => {
    return (
      <AuthorCard
        key={author.Id}
        author={author}
        redirectToAuthor={redirectToAuthor}
      />
    );
  });

  return (
    <Container fluid="md" className="container">
      <Row md={4} className="center container-row">
        {authors}
        <AddAuthorCard addAuthor={props.addAuthor} />
      </Row>
    </Container>
  );
}

export default AuthorsContainer;
