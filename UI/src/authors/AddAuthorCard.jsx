import { React } from "react";
import { Card, Button } from "react-bootstrap";

function AddAuthorCard(props) {
  const addAuthor = (event) => {
    event.preventDefault();
    props.addAuthor();
  };

  return (
    <Card border="light" style={{ width: "19rem" }} className="center">
      <Button
        variant="success"
        className="center card-add-button"
        onClick={addAuthor}>
        Add new author
      </Button>
    </Card>
  );
}

export default AddAuthorCard;
