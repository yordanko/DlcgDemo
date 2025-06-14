import  {  ListGroup, Button, Card } from "react-bootstrap";
import { useVideogames } from "../../lib/hooks/useVideogames";
import { useNavigate, useParams } from "react-router";

export default function GameDetail() {
  const {id} = useParams();
  const navigate = useNavigate();
  const {videoGame: game, isLoadingVideogames} = useVideogames(id);

  if (isLoadingVideogames) {
        return <div>Loading...</div>;
    }

  if(!game) {
        return <div className="text-center">No game details available.</div>;}
  
  return (
   
    <Card key={game.id} style={{borderRadius: 3, marginLeft:'30px', borderWidth:'0', width:'30rem' }}>
      <ListGroup.Item key={game.id}>
        <h2>{game.title}</h2>
        <Card.Img src={game.imageUrl} alt={game.title} style={{ width: '30rem', height: 'auto' }} />
        <Card.Body>
        <p><b>Genre: </b>{game.genre}</p>
        <p><b>Release Date: </b>{new Date(game.releaseDate).toLocaleDateString()}</p>
        <p><b>Description: </b>{game.description}</p>
        <p><b>Publisher: </b>{game.publisher}</p>
        <p><b>Platform: </b>{game.platform}</p>
        <p><b>Game Website: </b><Button href={game.url} target="_blank" className="btn-link" variant="info">Visit</Button></p>
        <Button variant="secondary" onClick={()=>navigate('/videogames')}>Cancel</Button>
        <Button variant="primary" style={{marginLeft:'10px'}} onClick={() => navigate(`/manage/${game.id}`)}>Edit</Button>
        </Card.Body>
      </ListGroup.Item>

      </Card>
   
  )
}