import { React } from "react";
import { Card, Button } from "react-bootstrap";
import "./AddBookCard.css";

function AddBookCard(props) {
  const addBook = (event) => {
    event.preventDefault();
    props.addBook();
  };

  return (
    <Card border="light" style={{ width: "19rem" }} className="center">
      <Button
        variant="success"
        className="center add-book-card-button"
        onClick={addBook}>
        Add new book
      </Button>
    </Card>
  );
}

export default AddBookCard;
