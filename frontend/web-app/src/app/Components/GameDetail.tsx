import  { Row, Col, ListGroup, Button } from "react-bootstrap";
import { useVideogames } from "../../lib/hooks/useVideogames";

type Props = {
    selectedGame: VideoGame;
    cancelSelectedGame?: () => void;
    openForm: (id?: string) => void;
}

export default function GameDetail({ selectedGame , cancelSelectedGame, openForm}: Props) {
  const {videoGames} = useVideogames();
  const game = videoGames?.find(game => game.id === selectedGame.id);
  

    if (!game) {
        return <div>Loading...</div>;
    }
  return (
    <>
    <div>GameDetail</div>
    
    <Row key={game.id}>
            <Col>
              <ListGroup.Item key={game.id}>
                <img src={game.imageUrl} alt={game.title} style={{ width: '100%', height: 'auto' }} />
                <h4>{game.title}</h4>
                <p>Genre: {game.genre}</p>
                <p>Release Date: {new Date(game.releaseDate).toLocaleDateString()}</p>
                <p>Description: {game.description}</p>
                <p>Publisher: {game.publisher}</p>
                <p>Platform: {game.platform}</p>
                <p><Button href={game.url} target="_blank" className="btn-link" variant="info">Game Website</Button></p>
                <Button variant="secondary" onClick={cancelSelectedGame}>Cancel</Button>
                <Button variant="primary" onClick={() => openForm(game.id)}>Edit</Button>
              </ListGroup.Item>
            </Col>
          </Row>
    </>
  )
}