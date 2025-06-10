import { ListGroup, Row, Col, Button } from "react-bootstrap";
import { useVideogames } from "../../lib/hooks/useVideogames";
import { useNavigate } from "react-router";


export default function GamesList() {
  const {videoGames, isPending, deleteVideoGame} = useVideogames();
  const navigate = useNavigate();
  if (isPending || !videoGames) {
    return <div className="text-center">Loading..</div>
  }
  return (
    <ListGroup>
        {videoGames.map((game) => (
          <Row key={game.id}>
            <Col>
              <ListGroup.Item key={game.id}>
                <h4>{game.title}</h4>
                <p>Genre: {game.genre}</p>
                <p>Release Date: {new Date(game.releaseDate).toLocaleDateString()}</p>
                <div className="pull-right">
                  <Button  variant="primary" onClick={()=>deleteVideoGame.mutate(game.id)} className="pull-right">
                    Delete
                  </Button>
                  <Button  variant="primary" onClick={()=> navigate(`/videogames/${game.id}`)}>
                    View
                  </Button>
                </div>
              </ListGroup.Item>
            </Col>
          </Row>
        ))}
      </ListGroup>
  )
}
