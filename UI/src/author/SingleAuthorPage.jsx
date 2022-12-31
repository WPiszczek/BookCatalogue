import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";

function SingleAuthorPage() {
  const { id } = useParams();
  const [author, setAuthor] = useState({});

  return <Container>Author page - {id}</Container>;
}

export default SingleAuthorPage;
