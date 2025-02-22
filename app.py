from flask import Flask, request, jsonify
import torch
import cv2
import numpy as np

app = Flask(__name__)

# Charger un modèle YOLO pré-entraîné
model = torch.hub.load('nomde modele ici ', 'yolov5s')  # Modèle YOLO pré-entraîné

@app.route('/predict', methods=['POST'])
def predict():
    # Vérifier si un fichier image a été soumis
    if 'image' not in request.files:
        return jsonify({"error": "Aucun fichier image fourni."}), 400

    file = request.files['image']
    
    # Lire l'image et la convertir en tableau numpy
    img = np.asarray(bytearray(file.read()), dtype=np.uint8)
    img = cv2.imdecode(img, cv2.IMREAD_COLOR)

    if img is None:
        return jsonify({"error": "Erreur lors de la lecture de l'image."}), 400

    # Prédiction des objets dans l'image
    results = model(img)

    # Récupérer les résultats sous forme de dictionnaire
    predictions = results.pandas().xywh[0].to_dict(orient="records")
    
    return jsonify(predictions)
