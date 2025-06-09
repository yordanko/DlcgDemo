import { ListGroup, Row, Col, Button } from "react-bootstrap";

type Props = {
    videoGames: VideoGame[];
    selectGame?: (id: string) => void;
}

export default function GamesList({ videoGames, selectGame }: Props) {

  return (
    <ListGroup>
        {videoGames.map((game) => (
          <Row key={game.id}>
            <Col>
              <ListGroup.Item key={game.id}>
                <h4>{game.title}</h4>
                <p>Genre: {game.genre}</p>
                <p>Release Date: {new Date(game.releaseDate).toLocaleDateString()}</p>
                <Button onClick={() => selectGame?.(game.id)} variant="primary">
                  View Details
                </Button>
              </ListGroup.Item>
            </Col>
          </Row>
        ))}
      </ListGroup>
  )
}
