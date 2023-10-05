-- Crear la tabla de Categorías
CREATE TABLE categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL
);

-- Insertar datos de prueba en la tabla de Categorías
INSERT INTO categorias (nombre) VALUES
('Laptops'),
('Smartphones'),
('Monitores'),
('Teclados'),
('Mouses');


-- Crear la tabla de Productos
CREATE TABLE productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    descripcion TEXT,
    precio INT NOT NULL,
    stock INT NOT NULL,
    categoria_id INT NOT NULL,
    FOREIGN KEY (categoria_id) REFERENCES categorias(id)
);


-- Insertar datos de prueba en la tabla de Productos
INSERT INTO productos (nombre, descripcion, precio, stock, categoria_id) VALUES
('Laptop Dell XPS 13', 'Laptop ultradelgada con pantalla InfinityEdge', 1299.00, 15, 1),
('iPhone 13 Pro', 'Smartphone de última generación con pantalla Super Retina XDR', 1499.00, 20, 2),
('Monitor LG 27UL850-W', 'Monitor 4K UHD con tecnología Nano IPS', 549.00, 10, 3),
('Teclado mecánico Corsair K95 RGB', 'Teclado mecánico con retroiluminación RGB personalizable', 99.00, 30, 4),
('Mouse Logitech G Pro Wireless', 'Mouse inalámbrico para gaming con sensor HERO', 79.00, 25, 5);

-- Crear la tabla de Usuarios
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    contraseña VARCHAR(255) NOT NULL
);

-- Insertar datos de prueba en la tabla de Usuarios
INSERT INTO usuarios (nombre, email, contraseña) VALUES
('Usuario1', 'usuario1@example.com', 'contraseña1'),
('Usuario2', 'usuario2@example.com', 'contraseña2'),
('Usuario3', 'usuario3@example.com', 'contraseña3');

-- Crear la tabla de Pedidos
CREATE TABLE pedidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    fecha_pedido TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

-- Insertar datos de prueba en la tabla de Pedidos
INSERT INTO pedidos (usuario_id) VALUES
(1),
(2),
(1);

-- Crear la tabla de Detalles de Pedidos
CREATE TABLE detalles_pedidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT NOT NULL,
    producto_id INT NOT NULL,
    cantidad INT NOT NULL,
    FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
    FOREIGN KEY (producto_id) REFERENCES productos(id)
);

-- Insertar datos de prueba en la tabla de Detalles de Pedidos
INSERT INTO detalles_pedidos (pedido_id, producto_id, cantidad) VALUES
(1, 1, 2),
(1, 3, 1),
(2, 2, 3),
(3, 4, 2);