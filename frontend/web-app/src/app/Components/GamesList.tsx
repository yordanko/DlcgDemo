import {  Row, Col, Button, Card, Container } from "react-bootstrap";
import { useVideogames } from "../../lib/hooks/useVideogames";
import { useNavigate } from "react-router";


export default function GamesList() {
  const {videoGames, isPending, deleteVideoGame} = useVideogames();
  const navigate = useNavigate();
  if (isPending || !videoGames) {
    return <div className="text-center">Loading..</div>
  }
  return (
    <Container >
      <Row>
        {videoGames.map((game) => (
          <Col xs-4>
          <Card key={game.id} style={{width:'15rem', borderRadius:3, marginTop:20, display:'flex', flexDirection:'column', gap:1}}>
            <Card.Img variant="top" src={game.imageUrl} style={{width:'15rem', height:'10rem' }}/>
            <Card.Body>
              <Card.Title>{game.title}</Card.Title>
              <Card.Text>
              <p>Genre: {game.genre}</p>
              <p>Release Date: {new Date(game.releaseDate).toLocaleDateString()}</p>
              </Card.Text>
              <div className="pull-right">
                <Button  variant="primary" onClick={()=>deleteVideoGame.mutate(game.id)} className="pull-right">
                  Delete
                </Button>
                <Button  variant="primary" style={{marginLeft:'1rem'}} onClick={()=> navigate(`/videogames/${game.id}`)}>
                  View
                </Button>
              </div>
            </Card.Body>
          </Card>
          </Col>
        ))}
      </Row>
    </Container>
  )
}
